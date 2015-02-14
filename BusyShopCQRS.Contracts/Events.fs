namespace BusyShopCQRS.Contracts.Events
open BusyShopCQRS.Contracts.Types
open BusyShopCQRS.Infrastructure
open System
open System.Runtime.Serialization

// Customer events
[<DataContract>]
type CustomerCreated = {
     [<field: DataMember(Name="id")>] Id: Guid; 
     [<field: DataMember(Name="name")>] Name: string 
 } with interface IEvent with member this.Id with get() = this.Id

type CustomerMarkedAsPreferred = {Id: Guid; Discount: int }
    with interface IEvent with member this.Id with get() = this.Id

// Product events
type ProductCreated = {Id: Guid; Name: string; Price: int }
    with interface IEvent with member this.Id with get() = this.Id

// Basket events
type BasketCreated = { Id: Guid; CustomerId: Guid; Discount: int}
    with interface IEvent with member this.Id with get() = this.Id

type ItemAdded = { Id: Guid; OrderLine: OrderLine}
    with 
    override this.ToString() = sprintf "Item added. Id: %O, %O" this.Id this.OrderLine
    interface IEvent with member this.Id with get() = this.Id

type CustomerIsCheckingOutBasket = { Id: Guid }
    with interface IEvent with member this.Id with get() = this.Id

type BasketCheckedOut = { Id: Guid; ShippingAddress: Address } 
    with interface IEvent with member this.Id with get() = this.Id

// Order events
type OrderCreated ={ Id: Guid; BasketId: Guid; OrderLines: OrderLine list }
    with interface IEvent with member this.Id with get() = this.Id

type ShippingProcessStarted = {Id: Guid}
    with interface IEvent with member this.Id with get() = this.Id

type OrderCancelled = {Id: Guid}
    with interface IEvent with member this.Id with get() = this.Id

type OrderShipped = {Id: Guid}
    with interface IEvent with member this.Id with get() = this.Id

type NeedsApproval = {Id: Guid}
    with interface IEvent with member this.Id with get() = this.Id

type OrderApproved = {Id: Guid}
    with interface IEvent with member this.Id with get() = this.Id
 
