<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class categories extends Model
{
    public function projects()
    {
        return $this->belongsTo(projects::class, 'projectid', 'projectid');
    }

}
