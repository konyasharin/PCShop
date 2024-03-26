import styles from './RegistrationPage.module.css';
import { useState } from 'react';
import Input from 'components/inputs/Input/Input.tsx';
import Btn from 'components/btns/Btn/Btn.tsx';
import { useDispatch } from 'react-redux';
import auth from 'api/auth.ts';
import TRegistrationData from 'types/auth/TRegistrationData.ts';

function RegistrationPage() {
  const [error, setError] = useState('');
  const [email, setEmail] = useState('');
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [repeatPassword, setRepeatPassword] = useState('');
  const dispatch = useDispatch();
  return (
    <div>
      <h2 className={styles.text}>Регистрация</h2>
      <h5 className={styles.error}>{error}</h5>
      <div className={styles.registrationBlock}>
        <Input
          className={styles.email}
          value={email}
          placeholder={'email'}
          onChange={(newEmail: string) => setEmail(newEmail)}
        />
        <Input
          className={styles.login}
          value={userName}
          placeholder={'Логин'}
          onChange={(newLogin: string) => setUserName(newLogin)}
        />
        <Input
          className={styles.password}
          value={password}
          placeholder={'Пароль'}
          onChange={(newPassword: string) => setPassword(newPassword)}
        />
        <Input
          className={styles.repeatPassword}
          value={repeatPassword}
          placeholder={'Повторите пароль'}
          onChange={(newRepeatPassword: string) =>
            setRepeatPassword(newRepeatPassword)
          }
        />
        <Btn
          className={styles.button}
          onClick={() => {
            if (password !== repeatPassword) {
              setError('Пароли не совпадают!');
              return;
            }
            auth<TRegistrationData>(
              { userName, password, email },
              '/User/signUp',
              dispatch,
            ).then(error => {
              if (error) {
                setError(error);
              } else {
                setError('');
              }
            });
          }}
        >
          Зарегистрироваться
        </Btn>
      </div>
    </div>
  );
}

export default RegistrationPage;
