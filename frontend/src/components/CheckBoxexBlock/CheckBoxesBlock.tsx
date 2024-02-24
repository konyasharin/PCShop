import React from 'react';
import TCheckBox from 'types/TCheckBox.ts';
import CheckBox from 'components/CheckBox/CheckBox.tsx';

type CheckBoxesBlockProps = {
  title: string;
  checkBoxes: TCheckBox[];
  onChange: (nameBlock: string, index: number, newIsActive: boolean) => void;
  checkBoxClassName?: string;
};

const CheckBoxesBlock: React.FC<CheckBoxesBlockProps> = props => {
  return (
    <>
      {...props.checkBoxes.map((checkBox, i) => {
        return (
          <CheckBox
            text={checkBox.text}
            isActive={checkBox.isActive}
            onChange={() => props.onChange(props.title, i, !checkBox.isActive)}
            className={props.checkBoxClassName ? props.checkBoxClassName : ''}
          />
        );
      })}
    </>
  );
};

export default CheckBoxesBlock;
