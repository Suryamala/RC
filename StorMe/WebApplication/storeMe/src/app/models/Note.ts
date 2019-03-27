import { Url } from 'url';
import { Label } from 'src/app/models/Label';
import { toDo } from './toDo';

export class Note {
    Id!: number;
    Title!: string;
    Label!: number;
    Note1!: string;
    Label1!: Label;
    ToDoNotes!: Array<toDo>;

    constructor(){
    this.Label1=new Label();
    this.ToDoNotes = new Array<toDo>();
    }
}