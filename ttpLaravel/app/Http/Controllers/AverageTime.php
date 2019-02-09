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
        //DB::enableQueryLog();

        $tasks = DB::table('tasksdoing')
        ->join('tasksplanned', 'tasksplanned.planid', '=', 'tasksdoing.planid')
        ->join('categories', 'categories.catid', '=', 'tasksplanned.catid')
        ->join('projects', 'projects.projectid', '=', 'categories.projectid')
        ->selectRaw('projectname, tasksplanned.catid, catname, planneddate, sum(duration) as sumDuration')
        ->groupBy('tasksplanned.catid', 'planneddate')
        ->orderby('projects.projectname', 'asc')
        ->whereDate('planneddate', '>=', '2019-01-15')
        ->get();

        //dd(DB::getQueryLog());

        $tasksListed = array();
        foreach ($tasks as $task) {
            if (!in_array($task->catid, $tasksListed)) {
                echo "<strong>" . $task->projectname . ":</strong> " . $task->catname .  " - ";
                $taskdetails = $tasks->where('catid', $task->catid);
                $arrDuration = array();
                foreach ($taskdetails as $taskdetail) {
                    echo $taskdetail->sumDuration . ", ";
                    array_push($arrDuration, $taskdetail->sumDuration);
                }
                $averageDuration = array_sum($arrDuration)/count($arrDuration);
                echo "(" . $averageDuration . ")";
                echo "<br>";
                array_push($tasksListed, $task->catid);
            }
        }
        
        //return view('averagetime', ['completedtasks' => $completedtasks]);
    }
}
