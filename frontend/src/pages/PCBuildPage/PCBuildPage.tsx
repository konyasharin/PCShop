import Container from '../../components/Container/Container.tsx';
import Scale from './Scale/Scale.tsx';
import { useState } from 'react';
import BuildsErrors from './BuildsErrors/BuildsErrors.tsx';
import PowerOfBuild from './PowerOfBuild/PowerOfBuild.tsx';

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
      <PowerOfBuild power={10} />
    </Container>
  );
}

export default PCBuildPage;
