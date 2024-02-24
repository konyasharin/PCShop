import React from 'react';
import TCheckBox from 'types/TCheckBox.ts';
import CheckBox from 'components/CheckBox/CheckBox.tsx';
import useCheckBoxes from 'hooks/useCheckBoxes.ts';
import styles from './CheckBoxesBlock.module.css';

type CheckBoxesBlockProps = {
  title: string;
  checkBoxes: TCheckBox[];
};

const CheckBoxesBlock: React.FC<CheckBoxesBlockProps> = props => {
  const { checkBoxesState, setCheckBoxIsActive } = useCheckBoxes(
    props.checkBoxes,
  );
  const checkBoxesArray = checkBoxesState.map((checkBox, i) => {
    return (
      <CheckBox
        text={checkBox.text}
        isActive={checkBox.isActive}
        onChange={() => setCheckBoxIsActive(i, !checkBox.isActive)}
        className={styles.checkBox}
      />
    );
  });
  return (
    <div>
      <h6 className={styles.title}>{props.title}</h6>
      {...checkBoxesArray}
    </div>
  );
};

export default CheckBoxesBlock;
