import styles from './RegistrationPage.module.css';
import { useState } from 'react';
import Input from 'components/Input/Input.tsx';
import Btn from 'components/Btn/Btn.tsx';
function RegistrationPage() {
  const [email, setEmail] = useState('');
  const [login, setLogin] = useState('');
  const [password, setPassword] = useState('');
  const [repeatePassword, setRepeatePassword] = useState('');
  return (
    <div>
      <h2 className={styles.text}>Регистрация</h2>
      <div className={styles.registrationBlock}>
        <Input
          className={styles.email}
          value={email}
          placeholder={'email'}
          onChange={(newEmail: string) => setEmail(newEmail)}
        />
        <Input
          className={styles.login}
          value={login}
          placeholder={'Логин'}
          onChange={(newLogin: string) => setLogin(newLogin)}
        />
        <Input
          className={styles.password}
          value={password}
          placeholder={'Пароль'}
          onChange={(newPassword: string) => setPassword(newPassword)}
        />
        <Input
          className={styles.repeatePassword}
          value={repeatePassword}
          placeholder={'Повторите пароль'}
          onChange={(newRepeatePassword: string) =>
            setRepeatePassword(newRepeatePassword)
          }
        />
        <Btn className={styles.button}>Зарегистрироваться</Btn>
      </div>
    </div>
  );
}

export default RegistrationPage;
