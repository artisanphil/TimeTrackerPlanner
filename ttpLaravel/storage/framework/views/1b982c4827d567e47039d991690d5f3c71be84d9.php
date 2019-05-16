<h1>Average Time per Project during last Month</h1>

    <ul>

    <?php $__currentLoopData = $completedtasks; $__env->addLoop($__currentLoopData); foreach($__currentLoopData as $task): $__env->incrementLoopIndices(); $loop = $__env->getLastLoop(); ?>

    <li>
        <?php echo e($task->tasks); ?>

    </li>

    <?php endforeach; $__env->popLoop(); $loop = $__env->getLastLoop(); ?>

    </ul>