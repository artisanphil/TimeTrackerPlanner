<?php

namespace App\Http\Controllers;

use DB;
use Illuminate\Http\Request;
use App\tasksdoing;
use App\tasksplanned;

class AverageTime extends Controller
{
    public function index()
    {
        /*
        $tasksplanned = tasksplanned::with('tasknames', 'tasknames.categories', 'tasknames.categories.projects')
        ->whereDate('planneddate', '>=', '2018-12-30')
        ->groupBy('planid', 'planneddate')
        ->get();

        //$tasksplanned->students->sortBy('whateverProperty');
        
        //$taskscompleted = $tasksplanned->tasksdoing->get();

        echo $tasksplanned;

        echo "<hr>";

        */

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
