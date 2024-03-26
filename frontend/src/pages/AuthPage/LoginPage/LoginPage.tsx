import styles from './LoginPage.module.css';
import { useState } from 'react';
import Input from 'components/inputs/Input/Input.tsx';
import Btn from 'components/btns/Btn/Btn.tsx';
import { NavLink } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import auth from 'api/auth.ts';
import TLoginData from 'types/auth/TLoginData.ts';

function LoginPage() {
  const [error, setError] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const dispatch = useDispatch();
  return (
    <div>
      <h2 className={styles.text}>ВХОД</h2>
      <h5 className={styles.error}>{error}</h5>
      <div className={styles.inputBlock}>
        <Input
          className={styles.login}
          value={email}
          placeholder={'Почта'}
          onChange={(newLogin: string) => setEmail(newLogin)}
        />
        <Input
          className={styles.password}
          value={password}
          placeholder={'Пароль'}
          onChange={(newPassword: string) => setPassword(newPassword)}
        />
        <Btn
          className={styles.button}
          onClick={() => {
            auth<TLoginData>({ email, password }, 'User/signIn', dispatch).then(
              error => {
                if (error) {
                  setError(error);
                } else {
                  setError('');
                }
              },
            );
          }}
        >
          войти
        </Btn>
        <NavLink to={'/registration'} className={styles.link}>
          У вас еще нет учетной записи? Зарегистрируйся сейчас!
        </NavLink>
      </div>
    </div>
  );
}

export default LoginPage;
