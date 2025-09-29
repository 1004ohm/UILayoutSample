using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoSingleton<PopupManager>
{
    /// <summary>
    /// 현재 에디터에서 게임 실행속도 향상을 위해 Enter Play Mode Option의 Reload Domain, Reload Scene을 모두 비활성화 처리한 상태임
    /// Reload Domain의 경우 비활성화시 static 필드/이벤트들이 초기화가 안 되는 문제가 있어 해당 함수를 통해 수동 초기화가 필요함
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void StaticInit()
    {
        //static 필드 초기화…
    }
}
