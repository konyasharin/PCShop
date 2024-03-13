import Input from 'components/Input/Input.tsx';
import { useState } from 'react';
import styles from './Orders.module.css';
import CheckBox from 'components/CheckBox/CheckBox.tsx';
import useCheckBoxes from 'hooks/useCheckBoxes.ts';
import OrderBlock from './OrderBlock/OrderBlock.tsx';
import EOrderStatuses from 'enums/EOrderStatuses.ts';
import ShowMoreBtn from 'components/btns/ShowMoreBtn/ShowMoreBtn.tsx';

function Orders() {
  const [id, setId] = useState('');
  const { checkBoxes, setCheckBoxIsActive } = useCheckBoxes([
    { text: 'Принят', isActive: false },
    { text: 'В обработке', isActive: false },
    { text: 'В пути', isActive: false },
    { text: 'Доставлен', isActive: false },
  ]);
  const checkBoxesBlock = checkBoxes.map((checkBox, i) => {
    return (
      <CheckBox
        className={styles.filter}
        isActive={checkBox.isActive}
        text={checkBox.text}
        onChange={() => setCheckBoxIsActive(i, !checkBox.isActive)}
      />
    );
  });
  return (
    <div className={styles.orders}>
      <Input
        placeholder={'Поиск по id'}
        value={id}
        onChange={newValue => setId(newValue)}
        type={'number'}
      />
      <div className={styles.filters}>{...checkBoxesBlock}</div>
      <OrderBlock
        className={styles.order}
        orderNumber={123456}
        status={EOrderStatuses.accepted}
      />
      <OrderBlock
        className={styles.order}
        orderNumber={123454}
        status={EOrderStatuses.delivered}
      />
      <ShowMoreBtn>Показать еще...</ShowMoreBtn>
    </div>
  );
}

export default Orders;
