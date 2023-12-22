import { useMemo, useReducer } from "react";
import { AppContext } from "../app";
import { DraftPoolApiClient } from "../shared/draftPoolApiClient";
import { PageLoadFunction } from "../pages";
import { useReducerWithContext } from "../../utils/reducerWithContext";
import { DraftStateReducer, getInitialState } from "./draftState";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { Breadcrumbs } from "../../components/breadcrumbs/breadcrumbs";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { NumberField } from "../../components/numberField/numberField";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { ConfirmationModal } from "../../components/modal/confirmationModal";
import { COLORS } from "../../style/constants";

interface DraftPageProps {
  appContext: AppContext;
  rosterId: number;
}

const teams = 2;

function DraftPage({ appContext, rosterId }: DraftPageProps) {
  const [state, update] = useReducer(
    DraftStateReducer, 
    getInitialState(teams), 
  )
  const draftPoolApiClient = useMemo(() => new DraftPoolApiClient(appContext.commandFetcher), [appContext.commandFetcher]);
  const header = <>
    <Breadcrumbs appContext={appContext}/>
  </>

  return <PowerUpLayout appContext={appContext} headerText="Draft Teams">
    <ContentWithHangingHeader header={header} headerHeight="48px">
      <Wrapper>
        <TeamContainer></TeamContainer>
        <DraftPoolContainer>
          <div 
            style={{ 
              display: 'flex',
              justifyContent: 'center',
              alignItems: 'flex-end', 
              gap: '16px'
            }}
          >
            <div style={{ width: '96px' }}>
                <FieldLabel htmlFor='teams'>Teams</FieldLabel>
                <NumberField 
                  id='teams' 
                  type='Defined' 
                  value={state.teams}
                  onChange={handlePlayersDraftingChange} 
                />
            </div>
            <Button onClick={() => {}} size='Small' variant="Fill">
              Generate Draft Pool
            </Button>
          </div>
        </DraftPoolContainer>
        <TeamContainer></TeamContainer>
      </Wrapper>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  function handlePlayersDraftingChange(value: number) {
    const shouldEdit = state.draftPool.length > 0
      ? appContext.openModalAsync(closeAndResolve => <ConfirmationModal 
          message='Are you sure you want to change the number of players drafting?'
          secondaryMessage='This will clear both the draft pool and any picks made'
          withDeny
          confirmVerb='Reset Draft Pool'
          closeDialog={closeAndResolve}
        />)
      : true;
    if(shouldEdit)
        update({ type: 'updateTeams', teams: value });
  }
}

export const loadDraftPage: PageLoadFunction = async (_, pageDef) => {
  if(pageDef.page !== 'DraftPage') throw '';

  return {
    title: 'Draft Teams',
    renderPage: appContext => <DraftPage appContext={appContext} rosterId={pageDef.rosterId} />
  }
}

const Wrapper = styled.div`
  display: flex;
  width: 100%;
  height: 100%;
  padding: 16px 32px;

`

const DraftPoolContainer = styled.div`
  flex: 2;
  padding: 0 16px;
`

const TeamContainer = styled.div`
  flex: 1;
  background-color: ${COLORS.jet.lighter_71};
  padding: 0 16px;
`