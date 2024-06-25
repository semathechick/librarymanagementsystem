import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Member } from '../../models/member';
import { MemberService } from '../../services/member.service';
import { AuthService } from '../../../core/services/Auth.service';


@Component({
  selector: 'app-user-account',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './user-account.component.html',
  styleUrl: './user-account.component.scss'
})
export class UserAccountComponent {

  memberForm!: FormGroup;
  loggedInMember: Member []=[];
  memberId:any;
  


  constructor(
    private memberService: MemberService,
    public authService: AuthService,
    private activeRoute: ActivatedRoute
  ){}

  ngOnInit(){
    this.getMember();
  }

  
  
  getMember(){
    this.memberId = this.activeRoute.snapshot.paramMap.get('id');
    this.memberService.getById(this.memberId).subscribe(response=>{
     this.loggedInMember=response.items;
  })
  }
}
