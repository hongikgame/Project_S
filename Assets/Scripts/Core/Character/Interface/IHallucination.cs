using UnityEngine;

public interface IHallucination
{
    public void OnHallucinationChanged(bool active);
    public abstract void OnHallucinationBegin();
    public abstract void OnHallucinationEnd();
}
