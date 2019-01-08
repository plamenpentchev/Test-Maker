import { Component, OnInit, OnChanges, SimpleChanges, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { IQuestion } from '../../../interfaces/IQuestion';


@Component({
  selector: 'question-list',
  templateUrl: './question-list.component.html',
  styleUrls: ['./question-list.component.css']
})
export class QuestionListComponent implements OnInit {

  @Input() public quiz: IQuiz;
  public questions: IQuestion[];
  public title: string;

  constructor(private http: HttpClient,
          @Inject('BASE_URL') private baseUrl: string,
          private router: Router)
          {
            this.questions = [];
          }

  ngOnInit() {
  }
  ngOnChanges(changes: SimpleChanges) {
      if (typeof changes['quiz'] !== undefined) {
        var change = changes['quiz'];
        if (!change.isFirstChange()) {
          this.loadData();
        }
      }
  }

  loadData() {
    this.http.get<IQuestion[]>(`${this.baseUrl}api/question/all/${this.quiz.QuizId}`)
    .subscribe(
      result => {
        this.questions = result;
        
      },
    error => console.error(error));
  }

  onCreate() {
    this.router.navigate(['/question/create', this.quiz.QuizId]);
  }

  onEdit(question: IQuestion) {
    console.log(question);
    this.router.navigate(['/question/edit', question.QuestionId]);
  }

  onDelete(question: IQuestion) {
    if (confirm('Do you really want to delete this question?')) {
      this.http.delete(`${this.baseUrl}api/question/${ question.QuestionId }`)
        .subscribe(
          result => {
            console.log(`Question mit der id [${question.QuestionId}] succsessfully deleted`);
            this.loadData();
          },
          error => console.error(error)
          );
    }
  }

}
