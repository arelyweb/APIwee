export class User {
  userName: string | null | undefined;
  lastName: string | null | undefined;
  password: string | null | undefined;
  roleId: number;

   constructor() {                
    this.userName = '' ;
    this.lastName = '';
    this.password = '';
    this.roleId = 1;

  }
}
