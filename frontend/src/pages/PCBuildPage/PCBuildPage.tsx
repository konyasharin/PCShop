import Container from 'components/Container/Container.tsx';
import Scale from './Scale/Scale.tsx';
import { useState } from 'react';
import BuildsError from './BuildsError/BuildsError.tsx';
import ChooseComponents from './ChooseComponents/ChooseComponents.tsx';
import CheckBox from 'components/CheckBox/CheckBox.tsx';
import useCheckBoxes from 'hooks/useCheckBoxes.ts';
import useRadios from 'hooks/useRadios.ts';
import Radio from 'components/Radio/Radio.tsx';

function PCBuildPage() {
  const [scalePercents] = useState(40);
  const { setCheckBoxIsActive, checkBoxesState } = useCheckBoxes([
    { text: 'Gigabyte', isActive: false },
    { text: 'Gigabyte', isActive: false },
    { text: 'Gigabyte', isActive: false },
    { text: 'Gigabyte', isActive: false },
  ]);
  const { setRadioIsActive, radiosState } = useRadios([
    { text: 'Gigabyte', isActive: false },
    { text: 'Gigabyte', isActive: false },
    { text: 'Gigabyte', isActive: false },
    { text: 'Gigabyte', isActive: false },
  ]);
  const checkBoxes = checkBoxesState.map((checkBox, i) => {
    return (
      <CheckBox
        text={checkBox.text}
        isActive={checkBox.isActive}
        onChange={() => setCheckBoxIsActive(i, !checkBox.isActive)}
      />
    );
  });
  const radios = radiosState.map((radio, i) => {
    return (
      <Radio
        text={radio.text}
        isActive={radio.isActive}
        onChange={() => setRadioIsActive(i)}
      />
    );
  });
  return (
    <Container>
      <Scale percents={scalePercents} />
      <BuildsError
        type={'Warning'}
        title={'Процессор'}
        description={'Выберите процессор из списка'}
      />
      <BuildsError
        type={'Error'}
        title={'Процессор'}
        description={'Выберите процессор из списка'}
      />
      <ChooseComponents />
      {...checkBoxes}
      {...radios}
    </Container>
  );
}

export default PCBuildPage;
