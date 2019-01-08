import { Component, OnInit, OnChanges, SimpleChanges, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { IQuestion } from '../../../interfaces/IQuestion';
@Component({
  selector: 'result-list',
  templateUrl: './result-list.component.html',
  styleUrls: ['./result-list.component.css']
})
export class ResultListComponent implements OnInit {

  @Input() public quiz: IQuiz;
  public results: IResult[];
  public title: string;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router) {
    this.results = [];
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
    this.http.get<IResult[]>(`${this.baseUrl}api/result/all/${this.quiz.QuizId}`)
      .subscribe(
        result => {
          this.results = result;
        },
        error => console.error(error));
  }

  onCreate() {
    this.router.navigate(['/result/create', this.quiz.QuizId]);
  }

  onEdit(result: IResult) {
    this.router.navigate(['/result/edit', result.ResultId]);
  }

  onDelete(r: IResult) {

    if (confirm('Do you really want to delete this result?')) {
      this.http.delete(`${this.baseUrl}api/result/${ r.ResultId }`)
        .subscribe(
          result => {
            console.log(`Question mit der id [${r.ResultId }] succsessfully deleted`);
            this.loadData();
          },
          error => console.error(error)
        );
    }
  }

}

