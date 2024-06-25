import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { TokenService } from '../services/token.service';
import { jwtDecode } from 'jwt-decode';
import { JWT_ROLES } from '../constants/jwtAttributes';
import { ToastrService } from 'ngx-toastr';


export const authGuard: CanActivateFn = (route, state) => {
  


  console.log("--------------------");
  const router = inject(Router);
  const tokenService = inject(TokenService);
  const toastr= inject(ToastrService);

  let rol:any;
  let token = tokenService.getToken();//Token servis'ten 'Token' değerini getir
  let hasToken = tokenService.hasToken();
  if (token == null) {
    router.navigateByUrl("/login");
    toastr.info("Giriş yapılması gerekmektedir.");
    return false;
  }
 
  
  else {
    console.log("HasToken:", hasToken)
    let decodedToken = jwtDecode<any>(token);//Jwt'yi decode et ve değişkene at

    const expirationTime = new Date(decodedToken.exp * 1000);
    const currentTime = new Date(Date.now());
    console.log("Şimdiki Zaman:", currentTime.getHours() + ":" + currentTime.getMinutes())
    console.log("Token Geçerlilik Süresi:", expirationTime.getHours() + ":" + expirationTime.getMinutes())
    if (expirationTime < currentTime) {
      console.log("Geçerlilik süresi bitti", expirationTime)
      tokenService.removeToken();
      return false;
    };


    //------------------------------------------------------//

    let userRoles: string[] = decodedToken[JWT_ROLES];//Okunan jwt'den role değerlerini oku ve değişkene at

    console.log("Tokendan gelen rol:", userRoles);

    //------------------------------------------------------//

    //if (tokenService.hasToken()) return true;//Token değeri içinde bir değer var mı diye bakıyor

    let requiredRoles: string[] = route.data['requiredRoles'] || [];//route için gerekli rolleri oku.
    console.log("Gerekli rol:", requiredRoles);

    requiredRoles.forEach((role) => {
      if (userRoles.includes(role)) {
        rol=role;
        console.log(role.toLowerCase(),"ile giriş yapıldı:true")
      }
      else{
        toastr.info("Giriş lazım")
        router.navigateByUrl("/login");
      }

    });
  }

  return true;
}; 