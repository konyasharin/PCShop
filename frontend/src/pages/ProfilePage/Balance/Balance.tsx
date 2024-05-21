import Btn from 'components/btns/Btn/Btn.tsx';
import styles from './Balance.module.css';
import Input from 'components/inputs/Input/Input.tsx';
import { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import editUser from 'api/user/editUser.ts';
import { setUserData } from 'store/slices/profileSlice.ts';
import { setIsLoading } from 'store/slices/loadingSlice.ts';

function Balance() {
  const [sum, setSum] = useState('');
  const dispatch = useDispatch();
  const userInfo = useSelector((state: RootState) => state.profile.userInfo);
  return (
    <>
      <div className={styles.block}>
        <h4>Всего:</h4>
        <h2 className={styles.price}>{userInfo ? userInfo.balance : ''}$</h2>
      </div>
      <div className={styles.end}>
        <Input
          className={styles.input}
          value={sum}
          placeholder={'Сумма пополнения'}
          onChange={(newSum: string) => setSum(newSum)}
        />
        <Btn
          className={styles.button}
          onClick={() => {
            if (userInfo) {
              dispatch(setIsLoading(true));
              editUser({
                ...userInfo,
                balance: userInfo.balance + +sum,
              }).then(response => {
                dispatch(setUserData(response.data));
              });
              dispatch(setIsLoading(false));
            }
          }}
        >
          Пополнить
        </Btn>
      </div>
    </>
  );
}

export default Balance;
