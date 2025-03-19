using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;
using System.Text;

public class ProductionHandler : MonoBehaviour
{
    private StringBuilder stringBuilder = new StringBuilder();
    private Building building;
    private BuildingData productionData;
    private bool isGenerator;
    private int currentAmount = 0;
    private int queuedAmount = 0;
    private float productionCompleteTime;

    private CancellationTokenSource productionCancellationTokenSource;
    private DateTime lastProductStartTime;
    private string buildingName;
    private bool isProducing;

    void Awake()
    {
        building = GetComponent<Building>();
        productionData = building.GetBuildingData();
        isGenerator = productionData.inputProducts.Count <= 0;
        buildingName = productionData.buildingName;
    }

    private void Start()
    {
        LoadProductionState();
        if(!isGenerator && queuedAmount <= 0) return;
        StartProduction().Forget();
    }

    private void LoadProductionState()
    {
        if(ES3.KeyExists(GetSaveKey("LastProductionStartTime")))
        {
            lastProductStartTime = ES3.Load<DateTime>(GetSaveKey("LastProductionStartTime"));
            isProducing = true;
        }
        else
        {
            lastProductStartTime = DateTime.UtcNow;
            isProducing = false;
        }

        queuedAmount = ES3.Load<int>(GetSaveKey("QueuedAmount"), 0);
        currentAmount = ES3.Load<int>(GetSaveKey("CurrentAmount"), 0);

        if (isProducing)
        {
            TimeSpan elapsedTime = DateTime.UtcNow - lastProductStartTime;
            ProcessElapsedTime(elapsedTime.TotalSeconds);
        }
        else
        {
            building.UpdateTime(-1, productionData.productionTime);
        }

        building.UpdateAmount(currentAmount, queuedAmount);
        if(currentAmount >= productionData.capacity) building.IsFull();
    }

    private void ProcessElapsedTime(double elapsedSeconds)
    {
        if (elapsedSeconds <= 0) return;

        if(isGenerator)
        {
            ProcessGeneratorElapsedTime(elapsedSeconds);
        }
        else
        {
            ProcessSpenderElapsedTime(elapsedSeconds);
        }
    }

    private void ProcessGeneratorElapsedTime(double elapsedSeconds)
    {
        int completedProductions = (int)(elapsedSeconds / productionData.productionTime);
        float remainingTime = (float)(elapsedSeconds % productionData.productionTime);
        
        currentAmount += completedProductions;
        currentAmount = Mathf.Min(currentAmount, productionData.capacity);

        if (currentAmount >= productionData.capacity)
        {
            building.IsFull();
            return;
        }

        if (remainingTime > 0)
        {
            isProducing = true;
            productionCompleteTime = Time.time + (productionData.productionTime - remainingTime);
            Debug.Log(productionCompleteTime - Time.time);
        }
    }

    private void ProcessSpenderElapsedTime(double elapsedSeconds)
    {
        int completedProductions = (int)(elapsedSeconds / productionData.productionTime);
        float remainingTime = (float)(elapsedSeconds % productionData.productionTime);

        queuedAmount = Mathf.Max(0, queuedAmount - completedProductions);
        currentAmount += completedProductions * productionData.outputAmount;
        currentAmount = Mathf.Min(currentAmount, productionData.capacity);

        if(currentAmount >= productionData.capacity)
        {
            building.IsFull();
            return;
        }

        if (remainingTime > 0 && queuedAmount > 0)
        {
            isProducing = true;
            productionCompleteTime = Time.time + (productionData.productionTime - remainingTime);
        }
    }

    private void SaveProductionState()
    {
        if (isProducing)
        {
            ES3.Save(GetSaveKey("LastProductionStartTime"), lastProductStartTime);
        }
        else
        {
            ES3.DeleteKey(GetSaveKey("LastProductionStartTime"));
        }
        ES3.Save(GetSaveKey("QueuedAmount"), queuedAmount);
        ES3.Save(GetSaveKey("CurrentAmount"), currentAmount);
    }

    private async UniTaskVoid StartProduction()
    {
        productionCancellationTokenSource = new CancellationTokenSource();

        try
        {
            while (true)
            {
                await Produce(productionCancellationTokenSource.Token);
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Production canceled");
        }
    }

    private async UniTask Produce(CancellationToken cancellationToken)
    {
        if(currentAmount >= productionData.capacity || (!isGenerator && queuedAmount == 0))
        {
            isProducing = false;
            await UniTask.Yield();
            return;
        }
        // Only set new production time if not already producing
        if (!isProducing)
        {
            Debug.Log("Starting production");
            isProducing = true;
            productionCompleteTime = Time.time + productionData.productionTime;
        }
        
        float remainingTime = productionCompleteTime - Time.time;
        lastProductStartTime = DateTime.UtcNow - TimeSpan.FromSeconds(productionData.productionTime - remainingTime);
        
        while(remainingTime > 0)
        {
            building.UpdateTime(remainingTime, productionData.productionTime);
            await UniTask.Delay(100, cancellationToken: cancellationToken);
            remainingTime = productionCompleteTime - Time.time;
        }

        if(!isGenerator && queuedAmount > 0)
        {
            queuedAmount--;
            if(queuedAmount == 0)
            {
                building.UpdateTime(-1, productionData.productionTime);
            }
        }
        
        currentAmount = Mathf.Min(currentAmount + productionData.outputAmount, productionData.capacity);
        building.UpdateAmount(currentAmount, queuedAmount);

        Managers.EventManager.Instance.ONOnProductionComplete();
        isProducing = false;

        if(currentAmount >= productionData.capacity)
        {
            building.IsFull();
            SaveProductionState();
            return;
        }

        SaveProductionState();
    }

    public void AddProduction()
    {
        if(queuedAmount == 0)
        {
            StartProduction().Forget();
        }
        queuedAmount++;
        ResourceManager.Instance.RemoveProductAmount(productionData.inputProducts[0].product.type, productionData.inputProducts[0].amount);
        building.UpdateAmount(currentAmount,queuedAmount);
        Managers.EventManager.Instance.ONOnProductionQueueChange();
    }

    public void RemoveProduction()
    {
        Debug.Log("Removing production");
        queuedAmount--;
        ResourceManager.Instance.AddProductAmount(productionData.inputProducts[0].product.type, productionData.inputProducts[0].amount);
        building.UpdateAmount(currentAmount,queuedAmount);
        Managers.EventManager.Instance.ONOnProductionQueueChange();
        if(queuedAmount == 0)
        {
            isProducing = false;
            productionCancellationTokenSource.Cancel();
            building.UpdateTime(-1, productionData.productionTime);
        }
        SaveProductionState();
    }

    public void Harvest()
    {
        var harvestedAmount = currentAmount;
        if (harvestedAmount <= 0) return;
        
        currentAmount = 0;
        ResourceManager.Instance.AddProductAmount(productionData.outputProduct.type, harvestedAmount);
        building.UpdateAmount(currentAmount, queuedAmount);
        
        SaveProductionState();
    }
    // public void AddProduction()
    // {
    //     if(queuedAmount < 1)
    //     {
    //         nextProductionTime = Time.time + productionData.productionTime;
    //         timer = 0;
    //     }
    //     queuedAmount++;
    //     ResourceManager.Instance.RemoveProductAmount(productionData.inputProducts[0].product.type, productionData.inputProducts[0].amount);
    //     building.UpdateAmount(currentAmount,queuedAmount);
    //     Managers.EventManager.Instance.ONOnProductionQueueChange();
    // }

    // public void RemoveProduction()
    // {
    //     Debug.Log("Removing production");
    //     queuedAmount--;
    //     ResourceManager.Instance.AddProductAmount(productionData.inputProducts[0].product.type, productionData.inputProducts[0].amount);
    //     building.UpdateAmount(currentAmount,queuedAmount);
    //     if(queuedAmount <= 0) timer = 0;
    //     Managers.EventManager.Instance.ONOnProductionQueueChange();
    // }

    // private void Produce()
    // {
    //     if(currentAmount == productionData.capacity) return;
    //     if(!isGenerator && queuedAmount == 0) return;
    //     timer += Time.deltaTime;
    //     building.UpdateTime(productionData.productionTime - timer, productionData.productionTime);
    //     if(Time.time < nextProductionTime) return;
    //     queuedAmount--;
    //     currentAmount += productionData.outputAmount;
    //     building.UpdateAmount(currentAmount,queuedAmount);
    //     timer = 0;
    //     Managers.EventManager.Instance.ONOnProductionComplete();
    //     if(currentAmount == productionData.capacity)
    //     {
    //         building.IsFull();
    //         return;
    //     } 
    //     nextProductionTime = Time.time + productionData.productionTime;
    //     if(isGenerator) return;
    // }

    public bool IsMaxCapacity()
    {
        return currentAmount + queuedAmount >= productionData.capacity;
    }

    public bool CanGetHarvested()
    {
        return currentAmount > 0;
    }

    public bool HasQueue()
    {
        return queuedAmount > 0;
    }

    void OnApplicationQuit()
    {
        SaveProductionState();
    }

    private string GetSaveKey(string dataName)
    {
        stringBuilder.Clear();
        stringBuilder.Append(buildingName);
        stringBuilder.Append(dataName);
        return stringBuilder.ToString();
    }
}
