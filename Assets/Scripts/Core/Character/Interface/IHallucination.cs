using UnityEngine;

public interface IHallucination
{
    public void OnHallucinationChanged(bool active);
    public void OnHallucinationBegin();
    public void OnHallucinationEnd();
}
