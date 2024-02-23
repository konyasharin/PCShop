import TCheckBox from 'types/TCheckBox.ts';
import { useState } from 'react';

function useRadios(radios: TCheckBox[]) {
  const [radiosState, setRadiosState] = useState<TCheckBox[]>(radios);

  function setRadioIsActive(index: number) {
    setRadiosState(
      radiosState.map((radio, i) => {
        radio.isActive = i === index;
        return radio;
      }),
    );
  }

  return { radiosState, setRadioIsActive };
}

export default useRadios;
