import styles from './LoginPage.module.css';
import { useState } from 'react';
import Input from 'components/Input/Input.tsx';
import Btn from 'components/Btn/Btn.tsx';
import { NavLink } from 'react-router-dom';

function LoginPage() {
  const [error] = useState('Неверный логин или пароль');
  const [login, setLogin] = useState('');
  const [password, setPassword] = useState('');
  return (
    <div>
      <h2 className={styles.text}>ВХОД</h2>
      <h5 className={styles.error}>{error}</h5>
      <div className={styles.inputBlock}>
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
        <Btn className={styles.button}>войти</Btn>
        <NavLink to={'/Registration'} className={styles.link}>
          У вас еще нет учетной записи? Зарегистрируйся сейчас!
        </NavLink>
      </div>
    </div>
  );
}

export default LoginPage;
