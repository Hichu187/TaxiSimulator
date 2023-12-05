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

    #region  Customer Quest
    public event UnityAction takeACall;
    public void TakeACall() => takeACall?.Invoke();
    public event UnityAction accepted;
    public void AcceptedCall() => accepted?.Invoke();
    public event UnityAction refuse;
    public void RefuseCall() => refuse?.Invoke();
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

    public event UnityAction getOut;
    public void GetOutCar() => getOut?.Invoke();
    #endregion
}