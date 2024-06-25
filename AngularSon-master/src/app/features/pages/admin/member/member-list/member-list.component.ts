import { Component, OnInit } from '@angular/core';
import { Member } from '../../../../models/member';
import { MemberService } from '../../../../services/member.service';
import { ResponseModel } from '../../../../models/responseModel';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.scss'
})
export class MemberListComponent implements OnInit{
constructor(private memberService:MemberService){

}
ngOnInit(): void {
  this.getMembers();
}
memberList!:Member[];
getMembers(){
  this.memberService.getAll().subscribe({
    next:(response:ResponseModel<Member>)=>{
      console.log("MemberResponse:",response);
      this.memberList=response.items;
    }
  })
}
}


