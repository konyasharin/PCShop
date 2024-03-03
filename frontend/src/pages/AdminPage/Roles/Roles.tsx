import Input from 'components/Input/Input.tsx';
import { useState } from 'react';
import styles from './Roles.module.css';
import useRadios from 'hooks/useRadios.ts';
import Radio from 'components/Radio/Radio.tsx';
import Btn from 'components/btns/Btn/Btn.tsx';

function Roles() {
  const [id, setId] = useState('');
  const { radios, setRadioIsActive } = useRadios([
    { text: 'Пользователь', isActive: false },
    { text: 'Продавец', isActive: false },
    { text: 'Администратор', isActive: false },
  ]);
  const radioBlocks = radios.map((radio, i) => {
    return (
      <Radio
        text={radio.text}
        onChange={() => setRadioIsActive(i)}
        isActive={radio.isActive}
        className={styles.radio}
      />
    );
  });
  return (
    <div className={styles.roles}>
      <Input
        placeholder={'Введите id пользователя'}
        value={id}
        onChange={newId => setId(newId)}
      />
      <div className={styles.radios}>{...radioBlocks}</div>
      <Btn>Выдать роль</Btn>
    </div>
  );
}

export default Roles;
