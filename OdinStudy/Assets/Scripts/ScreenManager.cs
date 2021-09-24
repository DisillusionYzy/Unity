using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour
{

    //在场景开始时自动打开的屏幕
    public Animator initiallyOpen;

    //当前打开的屏幕
    private Animator m_Open;

    //我们用来控制过渡的参数的哈希值。
    private int m_OpenParameterId;

    //在我们打开当前屏幕之前选择的游戏对象。
    //在关闭屏幕时使用，因此我们可以返回将其打开的按钮。
    private GameObject m_PreviouslySelected;

    //我们在检查时需要比对的动画器状态和过渡名称。
    const string k_OpenTransitionName = "Open";
    const string k_ClosedStateName = "Closed";

    public void OnEnable()
    {
        //我们将哈希缓存到 "Open" 参数，因此可提供给 Animator.SetBool。
        m_OpenParameterId = Animator.StringToHash(k_OpenTransitionName);

        //现在打开初始屏幕（如果已设置）。
        if (initiallyOpen == null)
            return;
        OpenPanel(initiallyOpen);
    }

    //关闭当前打开的面板并打开提供的面板。
    //还负责处理导航，设置新的选定元素。
    public void OpenPanel(Animator anim)
    {
        Debug.Log("In OpenPanel");
        if (m_Open == anim)
            return;
        Debug.Log("Try to Open");
        //激活新的屏幕层级视图，以便对其进行动画化。
        anim.gameObject.SetActive(true);
        //保存当前选定的用于打开此屏幕的按钮。（CloseCurrent 会对其进行修改）
        var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;
        //将屏幕移到前面。
        anim.transform.SetAsLastSibling();

        CloseCurrent();

        m_PreviouslySelected = newPreviouslySelected;

        //将新屏幕设置为打开的屏幕。
        m_Open = anim;
        //启动打开动画
        m_Open.SetBool(m_OpenParameterId, true);
        Debug.Log("Open new");

        //将新屏幕中的一个元素设置为新的选定元素。
        GameObject go = FindFirstEnabledSelectable(anim.gameObject);
        SetSelected(go);
    }

    //查找提供的层级视图中的第一个可选元素。
    static GameObject FindFirstEnabledSelectable(GameObject gameObject)
    {
        GameObject go = null;
        var selectables = gameObject.GetComponentsInChildren<Selectable>(true);
        foreach (var selectable in selectables)
        {
            if (selectable.IsActive() && selectable.IsInteractable())
            {
                go = selectable.gameObject;
                break;
            }
        }
        return go;
    }

    //关闭当前打开的屏幕
    //还负责处理导航。
    //将选定元素还原为打开当前屏幕之前使用的可选元素。
    public void CloseCurrent()
    {
        Debug.Log("Try to close current");
        if (m_Open == null)
            return;

        //启动关闭动画。
        m_Open.SetBool(m_OpenParameterId, false);
        Debug.Log("Close current");

        //将选定元素还原为打开当前屏幕之前使用的可选元素。
        SetSelected(m_PreviouslySelected);
        //关闭动画结束时启动协程以禁用该层级视图。
        StartCoroutine(DisablePanelDelayed(m_Open));
        //无打开屏幕。
        m_Open = null;
    }

    //协程将检测关闭动画何时结束，并会停用
    //层级视图。
    IEnumerator DisablePanelDelayed(Animator anim)
    {
        bool closedStateReached = false;
        bool wantToClose = true;
        while (!closedStateReached && wantToClose)
        {
            if (!anim.IsInTransition(0))
                closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);

            wantToClose = !anim.GetBool(m_OpenParameterId);

            yield return new WaitForEndOfFrame();
        }

        if (wantToClose)
            anim.gameObject.SetActive(false);
    }

    //选定提供的游戏对象
    //当使用鼠标/触摸时，我们实际上想要将其设置为先前选定的对象并且
    //现在不将任何对象设置为选定状态。
    private void SetSelected(GameObject go)
    {
        //选择游戏对象。
        EventSystem.current.SetSelectedGameObject(go);

        //如果我们现在正在使用键盘，那么我们便完成了所有设置。
        var standaloneInputModule = EventSystem.current.currentInputModule as StandaloneInputModule;
        if (standaloneInputModule != null)
            return;

        //由于我们使用的是指针设备，因此我们不希望选择任何内容。
        //但是如果用户切换到键盘，我们希望从提供的游戏对象开始导航。
        //所以，此处我们将当前的选定项设置为 null，因此提供的游戏对象成为 EventSystem 中的最后选定项。
        EventSystem.current.SetSelectedGameObject(null);
    }
}