//gsoap atxml service name:             AtXmlInterface
//gsoap atxml service documentation:    ARGCS Test Executive Interface
//gsoap atxml service location:         http://localhost:7014/

//gsoap atxml schema namespace:         urn:atxml

typedef char* xsd__string;

int atxml__Initialize(char *ProcType, char* ProcUuid, int Pid,
                             int* result);

int atxml__Close(int Handle, char* ProcUuid, int Pid,
                             int* result);


int atxml__RegisterInterUsed(int Handle,xsd__string InterUsage,
                             int* result);

int atxml__RetrieveTpsData(int Handle,
                           struct atxml__RetrieveTpsDataResponse {char* TpsName;
                                                                  char* TpsVersion;
                                                                  char* TpsFileName;
                                                                  char* RecommendedAction;
                                                                  int result;} &r);

int atxml__RegisterTSF(int Handle,
					   xsd__string TSFSignalDefinition,
                       xsd__string TSFLibrary,
                       xsd__string STDTSF,
                       xsd__string STDBSC,
                       int* result);

int atxml__ValidateRequirements(int Handle,
								xsd__string TestRequirements,
                                xsd__string Allocation, int BufferSize,
                                struct atxml__ValidateRequirementsResponse {xsd__string Availability;
                                                                            int result;} &r);

int atxml__RegisterRemoveSequence(int Handle,
								  xsd__string RemoveSequence,  int BufferSize,
                                  struct atxml__RegisterRemoveSequenceResponse {xsd__string Response;
                                                                                int result;} &r);

int atxml__RegisterApplySequence(int Handle,
								  xsd__string ApplySequence,  int BufferSize,
                                  struct atxml__RegisterApplySequenceResponse {xsd__string Response;
                                                                                int result;} &r);

int atxml__TestStationStatus(int Handle,int BufferSize,
                             struct atxml__TestStationStatusResponse {xsd__string TestStationStatus;
                                                                      int result;} &r);

int atxml__RegisterInstStatus(int Handle,
							  xsd__string InstStatus, int BufferSize,
                              struct atxml__RegisterInstStatusResponse {xsd__string Response;
                                                                        int result;} &r);

int atxml__RegisterTmaSelect(int Handle,xsd__string TmaList,
                             int* result);

int atxml__SubmitUutId(int Handle,char* UUT_Partnumber,
                       char* UUT_Serialnumber, int TmaBufferSize, int RaBufferSize,
                       struct atxml__SubmitUutIdResponse {int Action;
                                                          xsd__string cTmaList;
                                                          int TmaBufferSize;
                                                          xsd__string RecommendedActions;
                                                          int result;} &r);

int atxml__IssueSignal(int Handle,xsd__string SignalDescription, int BufferSize,
                       struct atxml__IssueSignalResponse {xsd__string Response;
                                                          int result;} &r);

int atxml__QueryInterStatus(int Handle,int BufferSize,
                            struct atxml__QueryInterStatusResponse {xsd__string InterStatus;
                                                                    int result;} &r);

int atxml__InvokeRemoveAllSequence(int Handle,int BufferSize,
                                   struct atxml__InvokeRemoveAllSequenceResponse {xsd__string Response;
                                                                                  int result;} &r);

int atxml__InvokeApplyAllSequence(int Handle,int BufferSize,
                                   struct atxml__InvokeApplyAllSequenceResponse {xsd__string Response;
                                                                                  int result;} &r);

int atxml__IssueTestResults(int Handle,xsd__string TestResults,
                            int TPS_Status, int BufferSize,
                            struct atxml__IssueTestResultsResponse {xsd__string Response;
                                                                    int result;} &r);

int atxml__IssueTestResultsFile(int Handle,xsd__string TestResultsFile,
                                int TPS_Status, int BufferSize,
                                struct atxml__IssueTestResultsFileResponse {xsd__string Response;
                                                                            int result;} &r);

int atxml__IssueIst(int Handle,xsd__string InstSelfTest, int BufferSize,
                       struct atxml__IssueIstResponse {xsd__string Response;
                                                          int result;} &r);

int atxml__IssueNativeCmds(int Handle,xsd__string InstrumentCmds, int BufferSize,
                           struct atxml__IssueNativeCmdsResponse {xsd__string Response;
                                                                  int result;} &r);

int atxml__IssueDriverFunctionCall(int Handle,xsd__string DriverFunction, int BufferSize,
                                   struct atxml__IssueDriverFunctionCallResponse {xsd__string Response;
                                                                                  int result;} &r);