import { useState } from 'react';
import TCheckBox from 'types/TCheckBox.ts';

function useCheckBoxes(checkBoxes: TCheckBox[]) {
  const [checkBoxesState, setCheckBoxesState] =
    useState<TCheckBox[]>(checkBoxes);

  function setCheckBoxIsActive(index: number, newIsActive: boolean) {
    setCheckBoxesState(
      checkBoxesState.map((stateElem, i) => {
        if (i === index) {
          stateElem.isActive = newIsActive;
        }
        return stateElem;
      }),
    );
  }

  return { setCheckBoxIsActive, checkBoxesState };
}

export default useCheckBoxes;
