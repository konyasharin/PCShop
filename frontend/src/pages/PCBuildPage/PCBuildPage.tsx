import Container from '../../components/Container/Container.tsx';
import Scale from './Scale/Scale.tsx';
import { useState } from 'react';
import BuildsErrors from './BuildsErrors/BuildsErrors.tsx';

function PCBuildPage() {
  const [scalePercents] = useState(40);
  return (
    <Container>
      <Scale percents={scalePercents} />
      <BuildsErrors
        type={'Warning'}
        title={'Процессор'}
        description={'Выберите процессор из списка'}
      />
      <BuildsErrors
        type={'Error'}
        title={'Процессор'}
        description={'Выберите процессор из списка'}
      />
    </Container>
  );
}

export default PCBuildPage;
