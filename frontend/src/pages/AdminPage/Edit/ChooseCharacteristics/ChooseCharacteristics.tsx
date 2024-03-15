import AccordionBtn from 'components/btns/AccordionBtn/AccordionBtn.tsx';
import styles from './ChooseCharacteristics.module.css';
import ChooseCharacteristic from './ChooseCharacteristic/ChooseCharacteristic.tsx';
import { useState } from 'react';

function ChooseCharacteristics() {
  const [characteristicsIsActive, setCharacteristicsIsActive] = useState([
    false,
  ]);
  return (
    <div className={styles.blocks}>
      <ChooseCharacteristic
        accordionBtn={
          <AccordionBtn
            text={'Видеокарты'}
            isActive={characteristicsIsActive[0]}
            className={styles.block}
            onClick={() =>
              setCharacteristicsIsActive([!characteristicsIsActive[0]])
            }
          />
        }
        characteristicBtns={[]}
      />
    </div>
  );
}

export default ChooseCharacteristics;
