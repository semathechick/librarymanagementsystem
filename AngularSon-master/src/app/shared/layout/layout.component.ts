import { Component } from '@angular/core';
import { TopbarComponent } from './topbar/topbar.component';
import { MiddlebarComponent } from "./middlebar/middlebar.component";
import { BottombarComponent } from "./bottombar/bottombar.component";


@Component({
    selector: 'app-layout',
    standalone: true,
    templateUrl: './layout.component.html',
    styleUrl: './layout.component.scss',
    imports: [TopbarComponent, MiddlebarComponent, BottombarComponent]
})
export class LayoutComponent {

}
