import Container from 'components/Container/Container.tsx';
import Scale from './Scale/Scale.tsx';
import { useState } from 'react';
import BuildsError from './BuildsError/BuildsError.tsx';
import ChooseComponents from './ChooseComponents/ChooseComponents.tsx';

function PCBuildPage() {
  const [scalePercents] = useState(40);
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
    </Container>
  );
}

export default PCBuildPage;
