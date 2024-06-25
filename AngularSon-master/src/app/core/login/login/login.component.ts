import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { LoginService } from '../../services/login.service';
import { Router, RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { LoginResponse } from '../../models/AccessToken';
import { TokenService } from '../../services/token.service';
import { jwtDecode } from 'jwt-decode';
import { Member } from '../../../features/models/member';
import { MemberService } from '../../../features/services/member.service';
import { Response } from '../../../features/models/response';
import { JWT_MAIL } from '../../constants/jwtAttributes';
import { AuthService } from '../../services/Auth.service';
import { LayoutComponent } from "../../../shared/layout/layout.component";
import { RegisterService } from '../../services/register.service';
import { BaseInputErrorsComponent } from '../../components/base-input-errors/base-input-errors.component';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-login',
    standalone: true,
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    imports: [CommonModule, ReactiveFormsModule, RouterModule, FormsModule,BaseInputErrorsComponent]
})
export class LoginComponent implements OnInit {
  passwordsignInHidden = true;
  //passwordsignUpHidden=true;

  constructor(
    private formBuilder: FormBuilder,
    private loginService: LoginService,
    private router: Router,
    private tokenService: TokenService,
    private memberService: MemberService,
    private authService:AuthService,
    private toastr: ToastrService
 
  ) {}


  ngOnInit(): void {
    this.getMembers();
    
    
  }

  userMailFound: boolean = false;
  userMail: string = '';
  emailList: string[] = [];
  memberList: Member[] = [];
  currentToken: any = this.tokenService.getToken();

  loginForm = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6),]],
    authenticatorCode: ['string']
  });

  onLogin() {
    const email = this.loginForm.value.email?? '';
    const password = this.loginForm.value.password?? '';
    const authenticatorCode = this.loginForm.value.authenticatorCode?? '';

    this.loginService.Login(email, password, authenticatorCode).subscribe((result: LoginResponse) => {
      result.email = email;
      localStorage.setItem('Token', result.accessToken.token);
      this.toastr.success('Başarılı bir şekilde giriş yaptınız.');
     // alert(result.email + " kullanıcısı giriş yaptı")
      this.onMemberLog();
      this.router.navigateByUrl('/homepage');
    });
  }

  onMemberLog() {
    this.currentToken = this.tokenService.getToken();
    let gelenToken = jwtDecode<any>(this.currentToken);
    this.userMail = gelenToken[JWT_MAIL];
    console.log("userMail:", this.userMail);
    console.log("Mailler:", this.emailList);

    if (this.memberList.length > 0) {
      for (let i = 0; i < this.emailList.length; i++) {
        if (this.emailList[i] == this.userMail) {
          this.userMailFound = true;
          break;
        }
      }

      if (this.userMailFound) {
        console.log("userMail, emailList içinde bulunuyor.");
        for (let i = 0; i < this.memberList.length; i++) {
          if (this.memberList[i].email == this.userMail) {
            console.log("Şu an sistemde giriş yapmış kullanıcı:", this.userMail);
            this.authService.loggedInMember=this.memberList[i];
            console.log("Tüm bilgi:",this.authService.loggedInMember);
          }
        }
      } else {
        console.log("userMail, emailList içinde bulunmuyor.");
      }
    } else {
      console.log("Üye listesi henüz yüklenmedi.");
    }
  }

  getMembers() {
    this.memberService.getAll().subscribe((response: Response<Member>) => {
      this.memberList = response.items;
      console.log("MemberList:", this.memberList);
      this.memberList.forEach(t => {
        this.emailList.push(t.email);
      });
    });
  }

  SignInPasswordVisibility() {
    this.passwordsignInHidden = !this.passwordsignInHidden;
  }
  /* SignUpPasswordVisibility() {
    this.passwordsignUpHidden = !this.passwordsignUpHidden;
  } */

 

}
