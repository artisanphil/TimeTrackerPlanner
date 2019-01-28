<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class tasknames extends Model
{
    protected $table = 'TaskNames';

    public function categories()
    {
        return $this->belongsTo(categories::class, 'categoryid', 'catid');
    }
}
