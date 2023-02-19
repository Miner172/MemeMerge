using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    [SerializeField] private TradeView _tradeTemplate;
    [SerializeField] private Transform _tradeContainer;

    public event UnityAction<int> OnTrySell;
    public void LoadShop(List<Character> charaters, bool[] openedLevels)
    {
        for (int i = 0; i < charaters.Count; i++)
        {
            TradeView view = Instantiate(_tradeTemplate, _tradeContainer);

            view.LoadView(charaters[i], openedLevels[i], i, this);
        }
    }

    public void TrySell(int id)
    {
        OnTrySell?.Invoke(id);
    }
}
