import TCheckBox from 'types/TCheckBox.ts';
import { useState } from 'react';

/**
 * Хук для управления чекбоксами
 * @param checkBoxesState Начальные чекбоксы
 */
function useCheckBoxes(checkBoxesState: TCheckBox[]) {
  const [checkBoxes, setCheckBoxes] = useState(checkBoxesState);
  function setCheckBoxIsActive(index: number, newIsActive: boolean) {
    setCheckBoxes(
      checkBoxes.map((checkBox, i) => {
        if (i === index) {
          checkBox.isActive = newIsActive;
        }
        return checkBox;
      }),
    );
  }
  return { checkBoxes, setCheckBoxIsActive };
}

export default useCheckBoxes;
