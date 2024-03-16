import TLoginData from 'types/auth/TLoginData.ts';

type TRegistrationData = TLoginData & {
  userName: string;
};

export default TRegistrationData;
