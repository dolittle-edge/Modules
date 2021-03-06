/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.DependencyInversion;
using Dolittle.Types;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace RaaLabs.TimeSeries.Modules.for_InputHandlers
{
    public class when_initializing_with_one_method_handler_in_the_system
    {
        const string input_name = "SomeInput";
        public class SomeData { }

        public class MyMethodHandler : ICanHandleMethods
        {
            public Input Input => input_name;

            [IotHubMethod]
            public Task ActionWithArgument(SomeData data)
            {
                return Task.CompletedTask;
            }

            [IotHubMethod]
            public Task ActionWithoutArgument()
            {
                return Task.CompletedTask;
            }

            [IotHubMethod]
            public Task<SomeData> FunctionWithoutArgument()
            {
                return Task.FromResult(new SomeData());
            }

            [IotHubMethod]
            public Task<SomeData> FunctionWithArgument(SomeData data)
            {
                return Task.FromResult(new SomeData());
            }
        }

        static Mock<ICommunicationClient> communication_client;
        static Mock<ITypeFinder> type_finder;
        static Mock<IContainer> container;
        static InputHandlers input_handlers;

        Establish context = () =>
        {
            communication_client = new Mock<ICommunicationClient>();

            type_finder = new Mock<ITypeFinder>();
            type_finder.Setup(_ => _.FindMultiple(typeof(ICanHandleMethods))).Returns(new [] {  typeof(MyMethodHandler) });

            container = new Mock<IContainer>();
            container.Setup(_ => _.Get(typeof(MyMethodHandler))).Returns(new MyMethodHandler());

            input_handlers = new InputHandlers(
                communication_client.Object,
                type_finder.Object,
                container.Object);
        };

        Because of = () => input_handlers.Initialize();

        It should_register_handler_for_action_without_parameters = () => 
            communication_client.Verify(_ => _
                .RegisterFunctionHandler(Moq.It.Is<ActionHandler>(a => a.Method.Name == "ActionWithoutArgument"))
            );

        It should_register_handler_for_action_with_parameters = () =>
            communication_client.Verify(_ => _
                .RegisterFunctionHandler(Moq.It.Is<ActionHandler<SomeData>>(a => a.Method.Name == "ActionWithArgument"))
            );

        It should_register_handler_for_function_without_parameters = () =>
            communication_client.Verify(_ => _
                .RegisterFunctionHandler(Moq.It.Is<FunctionHandler<SomeData>>(f => f.Method.Name == "FunctionWithoutArgument"))
            );

        It should_register_handler_for_function_with_parameters = () =>
            communication_client.Verify(_ => _
                .RegisterFunctionHandler(Moq.It.Is<FunctionHandler<SomeData, SomeData>>(f => f.Method.Name == "FunctionWithArgument"))
            );
    }
}