import EUserRoles from 'enums/EUserRoles.ts';

type TUser = {
  userName: string;
  email: string;
  role: EUserRoles;
  balance: number;
};

export default TUser;
