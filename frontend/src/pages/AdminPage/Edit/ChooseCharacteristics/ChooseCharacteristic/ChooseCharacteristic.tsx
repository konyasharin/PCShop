import React, { ReactNode } from 'react';

type ChooseCharacteristicProps = {
  accordionBtn: ReactNode;
  characteristicBtns: ReactNode[];
};

const ChooseCharacteristic: React.FC<ChooseCharacteristicProps> = props => {
  return (
    <>
      {props.accordionBtn}
      <div>{...props.characteristicBtns}</div>
    </>
  );
};

export default ChooseCharacteristic;
