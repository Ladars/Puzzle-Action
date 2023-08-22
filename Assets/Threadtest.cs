using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Threadtest : MonoBehaviour
{
    [ContextMenu("Run")]
    private async void testThread()
    {
        await Task.Run(
            () => {
                Thread.Sleep(2000);
                Debug.Log("I am Ready");
                Thread.Sleep(2000);
            }
        );
        Debug.Log("Fine");
    }
    [ContextMenu("Run1")]
    private void TestThread2()
    {
        List<Task> ts = new List<Task>();
        ts.Add(Task.Run(
            () =>
            {
                Thread.Sleep(2000);
                Debug.Log("I am Ready");
            }
        ));
        ts.Add(Task.Run(
           () =>
           {
               Thread.Sleep(3000);
               Debug.Log("I am fine");
           }
       ));
        Task.WhenAll(ts).ContinueWith(t=> Debug.Log("Let's go"));
    }
}
