import TCheckBox from 'types/TCheckBox.ts';
import { useState } from 'react';

function useRadios(radiosState: TCheckBox[]) {
  const [radios, setRadios] = useState(radiosState);
  function setRadioIsActive(index: number) {
    setRadios(
      radios.map((radio, i) => {
        radio.isActive = i === index;
        return radio;
      }),
    );
  }
  return { radios, setRadioIsActive };
}

export default useRadios;
