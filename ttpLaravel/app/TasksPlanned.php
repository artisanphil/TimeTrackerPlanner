<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class tasksplanned extends Model
{
    protected $table = 'TasksPlanned';

    public function tasksdoing()
    {
        return $this->hasMany(tasksdoing::class, 'planid', 'planid');
    }

    public function tasknames()
    {
        return $this->belongsTo(tasknames::class, 'taskid', 'taskid');
    }
}
