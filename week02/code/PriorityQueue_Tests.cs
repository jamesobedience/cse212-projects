using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items with different priorities and dequeue once.
    // Items added in this order: A (priority 1), B (priority 3), C (priority 2).
    // Expected Result: Item B should be removed first because it has the highest priority.
    // Defect(s) Found: Dequeue does not always remove the item with the highest priority.
    // Instead, it may remove the first item added or an incorrect item based on position
    // rather than priority.
    public void TestPriorityQueue_HighestPriorityRemoved()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("B", result);
    }

    [TestMethod]
    // Scenario: Add multiple items with the same highest priority.
    // Items added in this order: A (priority 2), B (priority 5), C (priority 5), D (priority 1).
    // Expected Result: B should be removed first because it has the highest priority
    // and appears first among the tied highest-priority items (FIFO behavior).
    // Defect(s) Found: When multiple items share the highest priority, the queue does not
    // consistently remove the item that was added first.
    public void TestPriorityQueue_FIFOForSamePriority()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 5);
        priorityQueue.Enqueue("D", 1);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("B", result);
    }

    [TestMethod]
    // Scenario: Dequeue repeatedly until the queue is empty.
    // Items added in this order: A (1), B (3), C (2).
    // Expected Result: Items should be removed in this order: B, C, A.
    // Defect(s) Found: The queue does not maintain correct priority ordering
    // across multiple dequeue operations.
    public void TestPriorityQueue_MultipleDequeues()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty priority queue.
    // Expected Result: An InvalidOperationException should be thrown with the message
    // "The queue is empty."
    // Defect(s) Found: Either no exception is thrown, the wrong exception type is thrown,
    // or the exception message does not match the required message.
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                              e.GetType(), e.Message)
            );
        }
    }
}
