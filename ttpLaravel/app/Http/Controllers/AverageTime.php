<?php

namespace App\Http\Controllers;

use DB;
use Illuminate\Http\Request;
use App\tasksdoing;

class AverageTime extends Controller
{
    public function index(tasksdoing $tasksdoing)
    {
        //DB::enableQueryLog();

        $tasks = DB::table('tasksdoing')
        ->join('tasksplanned', 'tasksplanned.planid', '=', 'tasksdoing.planid')
        ->join('tasknames', 'tasknames.taskid', '=', 'tasksplanned.taskid')
        ->join('categories', 'categories.catid', '=', 'tasknames.categoryid')
        ->join('projects', 'projects.projectid', '=', 'categories.projectid')
        ->selectRaw('projectname, catname, planneddate, tasksdoing.planid, tasksplanned.taskid, tasksplanned.estimation, taskname, sum(duration) as sumDuration')
        ->groupBy('tasksdoing.planid', 'planneddate')
        ->orderby('projects.projectname', 'asc')
        ->whereDate('planneddate', '>=', '2018-12-30')
        ->get();

        //dd(DB::getQueryLog());

        $tasksListed = array();
        foreach($tasks as $task)
        {
            if(!in_array($task->taskid, $tasksListed))
            {
                echo "<strong>" . $task->projectname . ":</strong> " . $task->taskname . " (" . $task->catname .  ") - "; 
                $taskdetails = $tasks->where('taskid', $task->taskid);
                $arrDuration = array();
                foreach($taskdetails as $taskdetail)
                {
                    echo $taskdetail->sumDuration . ", ";
                    array_push($arrDuration, $taskdetail->sumDuration);
                }
                $averageDuration = array_sum($arrDuration)/count($arrDuration);
                echo "(" . $averageDuration . ")";
                echo "<br>";
                array_push($tasksListed, $task->taskid);
            }
        }
        
        //return view('averagetime', ['completedtasks' => $completedtasks]);
    }

}
