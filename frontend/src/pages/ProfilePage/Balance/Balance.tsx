import Btn from 'components/btns/Btn/Btn.tsx';
import styles from './Balance.module.css';
import Input from 'components/inputs/Input/Input.tsx';
import { useState } from 'react';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';

function Balance() {
  const [sum, setSum] = useState('');
  const balance = useSelector(
    (state: RootState) => state.profile.userInfo?.balance,
  );
  return (
    <>
      <div className={styles.block}>
        <h4>Всего:</h4>
        <h2 className={styles.price}>{balance}$</h2>
      </div>
      <div className={styles.end}>
        <Input
          className={styles.input}
          value={sum}
          placeholder={'Сумма пополнения'}
          onChange={(newSum: string) => setSum(newSum)}
        />
        <Btn className={styles.button}>Пополнить</Btn>
      </div>
    </>
  );
}

export default Balance;
