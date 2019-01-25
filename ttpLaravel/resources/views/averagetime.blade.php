<h1>Average Time per Project during last Month</h1>

    <ul>

    @foreach ($completedtasks as $task)

    <li>
        {{ $task->tasks->taskname }}
    </li>

    @endforeach

    </ul>