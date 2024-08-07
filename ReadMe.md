使用.Net Framework4.8+CommunityToolkit+Handycontrol+EF6

### CommunityToolkit的用法

1. ## PackageReference 的好处

   - **在一个位置管理所有项目依赖项**：就像项目到项目引用和程序集引用一样，NuGet 包引用（使用节点）直接在项目文件中进行管理，而不是使用单独的 packages.config 文件。`PackageReference`
   - **顶级依赖项的整洁视图**：与 packages.config 不同，PackageReference 仅列出直接安装在项目中的 NuGet 包。因此，NuGet 包管理器 UI 和项目文件不会因下层依赖项而混乱
   
   ### CommunityToolkit.Mvvm.ComponentModel
   
   - **ObservableObject**: 一个基类，提供了对属性更改通知的支持。它实现了 `INotifyPropertyChanged` 接口，允许绑定的属性在更改时发送通知。
   - **ObservableRecipient**: 一个类，继承自 `ObservableObject`，添加了对消息接收的支持。它用于接收和响应消息。
   - **ObservableValidator**: 一个类，用于提供属性验证功能。它可以在属性值更改时执行验证逻辑，并通知绑定系统验证结果。
   
   ### CommunityToolkit.Mvvm.DependencyInjection
   
   - **Ioc**: 一个依赖注入容器的基本实现，用于管理对象的生命周期和依赖关系。
   
   ### CommunityToolkit.Mvvm.Input
   
   - **RelayCommand**: 一个简单的命令实现，可以执行无参数的操作。
   - **RelayCommand<T>**: 泛型版本的 `RelayCommand`，允许执行带有一个参数的操作。
   - **AsyncRelayCommand**: 一个异步命令实现，用于执行异步操作。
   - **AsyncRelayCommand<T>**: 泛型版本的 `AsyncRelayCommand`，允许执行带有一个参数的异步操作。
   - **IRelayCommand**: `RelayCommand` 的接口定义，表示一个可以执行的命令。
   - **IRelayCommand<T>**: 泛型版本的 `IRelayCommand`。
   - **IAsyncRelayCommand**: `AsyncRelayCommand` 的接口定义，表示一个可以执行异步操作的命令。
   - **IAsyncRelayCommand<T>**: 泛型版本的 `IAsyncRelayCommand`。
   
   ### CommunityToolkit.Mvvm.Messaging
   
   - **IMessenger**: 消息系统的基础接口，允许对象之间进行松耦合的通信。
   - **WeakReferenceMessenger**: 一个实现 `IMessenger` 的消息系统，它使用弱引用来引用消息接收者，以避免内存泄漏。
   - **StrongReferenceMessenger**: 一个使用强引用的消息系统，与 `WeakReferenceMessenger` 相比，它不会因引用问题导致消息接收者被垃圾回收。
   - **IRecipient<TMessage>**: 消息接收者的接口，定义了接收消息的方法。
   
   ### CommunityToolkit.Mvvm.Messaging.Messages
   
   - **PropertyChangedMessage<T>**: 表示属性更改的消息，通常用于通知观察者属性值已更改。
   - **RequestMessage<T>**: 表示请求的一条消息，通常用于请求某些操作或数据。
   - **AsyncRequestMessage<T>**: 异步版本的 `RequestMessage<T>`，用于执行异步请求。
   - **CollectionRequestMessage<T>**: 用于集合操作的请求消息。
   - **AsyncCollectionRequestMessage<T>**: 异步版本的 `CollectionRequestMessage<T>`，用于执行异步集合操作。
   - **ValueChangedMessage<T>**: 表示值已更改的消息，类似于 `PropertyChangedMessage<T>`，但专门用于值更改的场景。



### EF6

不管是数据库先行还是代码先行，都是用数据库优化生成代码，这样会省掉很多中间配置环节