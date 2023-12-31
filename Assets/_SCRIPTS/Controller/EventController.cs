using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    public static EventController instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public event UnityAction startGame;
    public void GameStart() => startGame?.Invoke();
    public event UnityAction startDrive;
    public void StartDrive() => startDrive?.Invoke();

    public event UnityAction<float> damage;
    public void Damage(float value) => damage?.Invoke(value);

    public event UnityAction repair;
    public void Repair() => repair?.Invoke();


    #region  Customer Quest
    public event UnityAction takeACall;
    public void TakeACall() => takeACall?.Invoke();
    public event UnityAction openPhone;
    public void OpenPhone() => openPhone?.Invoke();
    public event UnityAction accepted;
    public void AcceptedCall() => accepted?.Invoke();
    public event UnityAction refuse;
    public void RefuseCall() => refuse?.Invoke();
    public event UnityAction carStop;
    public void CarStop() => carStop?.Invoke();
    public event UnityAction pickUpCustomer;
    public void PickUpCustomer() => pickUpCustomer?.Invoke();
    public event UnityAction completeTrip;
    public void CompleteTheTrip() => completeTrip?.Invoke();
    #endregion

    #region  Select Vehicle
    public event UnityAction<int> selectById;
    public void SelectVehicle(int id) => selectById?.Invoke(id);
    #endregion

    #region Human
    public event UnityAction getIn;
    public void GetInCar() => getIn?.Invoke();

    public event UnityAction closeDoor;
    public void CloseDoor() => closeDoor?.Invoke();

    public event UnityAction setupDone;
    public void SetupDone() => setupDone?.Invoke();
    public event UnityAction getOut;
    public void GetOutCar() => getOut?.Invoke();
    #endregion


    #region  ParkingMode
    public event UnityAction parkingDone;
    public void ParkingDone() => parkingDone?.Invoke();
    #endregion


    public event UnityAction<int> addCash;
    public void AddCash(int value) => addCash?.Invoke(value);

}
