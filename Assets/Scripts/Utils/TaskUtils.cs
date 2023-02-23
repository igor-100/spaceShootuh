using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;

namespace SpaceShootuh.Utils
{
    public static class TaskUtils
    {
        public static async UniTask ToUniTaskWithImmediateCancel(this Tween tween, CancellationToken token)
        {
            await tween.ToUniTask(TweenCancelBehaviour.Kill, token);
        }

        public static void CancelToken(CancellationTokenSource cancellationTokenSource)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}