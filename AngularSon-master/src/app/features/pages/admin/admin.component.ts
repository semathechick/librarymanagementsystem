
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TopbarComponent } from "../../../shared/layout/topbar/topbar.component";
import { MiddlebarComponent } from "../../../shared/layout/middlebar/middlebar.component";


@Component({
    selector: 'app-admin',
    standalone: true,
    templateUrl: './admin.component.html',
    styleUrl: './admin.component.scss',
    imports: [RouterModule, TopbarComponent, MiddlebarComponent]
})
export class AdminComponent {

}
