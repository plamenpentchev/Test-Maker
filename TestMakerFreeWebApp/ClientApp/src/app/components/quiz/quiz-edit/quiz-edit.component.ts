import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router, Route } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-quiz-edit',
  templateUrl: './quiz-edit.component.html',
  styleUrls: ['./quiz-edit.component.css']
})
export class QuizEditComponent implements OnInit {
  title: string;
  quiz: IQuiz;
  editMode: boolean;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string)
  {
    this.quiz = <IQuiz>{};
    var id = +this.route.snapshot.params['id'];
    if (id) {
      this.editMode = true;
      this.http.get<IQuiz>(`${this.baseUrl}api/quiz/${id}`).subscribe(
        result => {
          this.quiz = <IQuiz>result;
          this.title = `Editing '${this.quiz.Title}'`;
        },
        error => console.error(error)
      );
    }
    else {
      this.editMode = false;
      this.title = `Create a new quiz`;
    }
  }

  ngOnInit() {
  }

  onSubmit(quiz: IQuiz) {
    var url = `${this.baseUrl}api/quiz`;
    if (!quiz.Title || !quiz.Description || !quiz.Text) {
      console.error(new Error(`Missing title, description and/or text for the quiz`));
      return;
    }
    if (this.editMode) {
      
      this.http.post<IQuiz>(url, quiz).subscribe(
        result => {
          var v = result;
          console.log(`The quiz mit der id ${v.QuizId} was successfully updated.`);
          this.router.navigate(['home']);
        },
        error => {
          console.error(error);
        }
      );
    }
    else {

      this.http.put<IQuiz>(url, quiz).subscribe(
        result => {
          var v = result;
          console.log(`Quiz with the id ${v.QuizId} successfully created.`);
          this.router.navigate(['home']);
        },
        error => {
          console.error(error);
        }
      );

    }
  }
  onBack() {
    this.router.navigate(['home']);
  }

}
