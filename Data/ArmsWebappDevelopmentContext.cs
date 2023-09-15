using Microsoft.EntityFrameworkCore;
using ArmstrongServer.Models.ConfigModels;
using ArmstrongServer.Models.DataModels;

namespace ArmstrongServer.Data;

public partial class ArmsWebappDevelopmentContext : DbContext
{
  public ArmsWebappDevelopmentContext()
  {
  }

  public ArmsWebappDevelopmentContext(DbContextOptions<ArmsWebappDevelopmentContext> options)
      : base(options)
  {
  }

  public virtual DbSet<ArInternalMetadatum> ArInternalMetadata { get; set; }

  public virtual DbSet<Building> Buildings { get; set; }

  public virtual DbSet<Channel> Channels { get; set; }

  public virtual DbSet<Device> Devices { get; set; }

  public virtual DbSet<DeviceComponent> DeviceComponents { get; set; }

  public virtual DbSet<DeviceModel> DeviceModels { get; set; }

  public virtual DbSet<DeviceRegGroup> DeviceRegGroups { get; set; }

  public virtual DbSet<Division> Divisions { get; set; }

  public virtual DbSet<History> Histories { get; set; }

  public virtual DbSet<Inspection> Inspections { get; set; }

  public virtual DbSet<Manufacturer> Manufacturers { get; set; }

  public virtual DbSet<MeasurementClass> MeasurementClasses { get; set; }

  public virtual DbSet<MeasurementGroup> MeasurementGroups { get; set; }

  public virtual DbSet<Organization> Organizations { get; set; }

  public virtual DbSet<Post> Posts { get; set; }

  public virtual DbSet<Room> Rooms { get; set; }

  public virtual DbSet<SchemaMigration> SchemaMigrations { get; set; }

  public virtual DbSet<Server> Servers { get; set; }

  public virtual DbSet<Service> Services { get; set; }

  public virtual DbSet<SupplementaryKit> SupplementaryKits { get; set; }

  public virtual DbSet<User> Users { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    var connectionStringBuilder = new Npgsql.NpgsqlConnectionStringBuilder
    {
      Host = AppSettings.AppSqlConnectionSettings.Host,
      Username = AppSettings.AppSqlConnectionSettings.Username,
      Password = AppSettings.AppSqlConnectionSettings.Password,
      Database = AppSettings.AppSqlConnectionSettings.Database
    };

    optionsBuilder.UseNpgsql(connectionStringBuilder.ConnectionString);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<ArInternalMetadatum>(entity =>
    {
      entity.HasKey(e => e.Key).HasName("ar_internal_metadata_pkey");

      entity.ToTable("ar_internal_metadata");

      entity.Property(e => e.Key)
              .HasColumnType("character varying")
              .HasColumnName("key");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");
      entity.Property(e => e.Value)
              .HasColumnType("character varying")
              .HasColumnName("value");
    });

    modelBuilder.Entity<Building>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("buildings_pkey");

      entity.ToTable("buildings");

      entity.HasIndex(e => e.OrganizationId, "index_buildings_on_organization_id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");

      entity.HasOne(d => d.Organization).WithMany(p => p.Buildings)
              .HasForeignKey(d => d.OrganizationId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_6c5ed8c27f");
    });

    modelBuilder.Entity<Channel>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("channels_pkey");

      entity.ToTable("channels");

      entity.HasIndex(e => e.DeviceId, "index_channels_on_device_id");

      entity.HasIndex(e => e.RoomId, "index_channels_on_room_id");

      entity.HasIndex(e => e.ServerId, "index_channels_on_server_id");

      entity.HasIndex(e => e.ServiceId, "index_channels_on_service_id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.ChannelId).HasColumnName("channel_id");
      entity.Property(e => e.Consumptiom)
              .HasDefaultValueSql("1.0")
              .HasColumnName("consumptiom");
      entity.Property(e => e.ConversionCoefficient)
              .HasDefaultValueSql("0.0")
              .HasColumnName("conversion_coefficient");
      entity.Property(e => e.CreatedAt)
              .HasColumnName("created_at");
      entity.Property(e => e.DeviceId).HasColumnName("device_id");
      entity.Property(e => e.EmergencyLimit)
              .HasDefaultValueSql("2.0")
              .HasColumnName("emergency_limit");
      entity.Property(e => e.EventCount)
              .HasDefaultValueSql("0")
              .HasColumnName("event_count");
      entity.Property(e => e.EventDatetime)
              .HasColumnName("event_datetime");
      entity.Property(e => e.EventErrorCount)
              .HasDefaultValueSql("0")
              .HasColumnName("event_error_count");
      entity.Property(e => e.EventImpulseValue)
              .HasDefaultValueSql("0.0")
              .HasColumnName("event_impulse_value");
      entity.Property(e => e.EventNotSystemValue)
              .HasDefaultValueSql("0.0")
              .HasColumnName("event_not_system_value");
      entity.Property(e => e.EventSystemValue)
              .HasDefaultValueSql("0.0")
              .HasColumnName("event_system_value");
      entity.Property(e => e.IsOnline).HasColumnName("is_online");
      entity.Property(e => e.IsSpecialControl)
              .HasDefaultValueSql("false")
              .HasColumnName("is_special_control");
      entity.Property(e => e.LocationDescription).HasColumnName("location_description");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.PreEmergencyLimit)
              .HasDefaultValueSql("1.0")
              .HasColumnName("pre_emergency_limit");
      entity.Property(e => e.RoomId).HasColumnName("room_id");
      entity.Property(e => e.SelfBackground)
              .HasDefaultValueSql("0.0")
              .HasColumnName("self_background");
      entity.Property(e => e.ServerId).HasColumnName("server_id");
      entity.Property(e => e.ServiceId).HasColumnName("service_id");
      entity.Property(e => e.State)
              .HasDefaultValueSql("'normal'::character varying")
              .HasColumnType("character varying")
              .HasColumnName("state");
      entity.Property(e => e.UpdatedAt)
              .HasColumnName("updated_at");

      entity.HasOne(d => d.Device).WithMany(p => p.Channels)
              .HasForeignKey(d => d.DeviceId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_3484cf4de2");

      entity.HasOne(d => d.Room).WithMany(p => p.Channels)
              .HasForeignKey(d => d.RoomId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_d2d6bb4242");

      entity.HasOne(d => d.Server).WithMany(p => p.Channels)
              .HasForeignKey(d => d.ServerId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_75998992af");

      entity.HasOne(d => d.Service).WithMany(p => p.Channels)
              .HasForeignKey(d => d.ServiceId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_6a40f1b796");
    });

    modelBuilder.Entity<Device>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("devices_pkey");

      entity.ToTable("devices");

      entity.HasIndex(e => e.DeviceModelId, "index_devices_on_device_model_id");

      entity.HasIndex(e => e.DeviceRegGroupId, "index_devices_on_device_reg_group_id");

      entity.HasIndex(e => e.InventoryId, "index_devices_on_inventory_id").IsUnique();

      entity.HasIndex(e => e.RoomId, "index_devices_on_room_id");

      entity.HasIndex(e => e.ServiceId, "index_devices_on_service_id");

      entity.HasIndex(e => e.SupplementaryKitId, "index_devices_on_supplementary_kit_id");

      entity.HasIndex(e => e.TabelId, "index_devices_on_tabel_id").IsUnique();

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.DeviceModelId).HasColumnName("device_model_id");
      entity.Property(e => e.DeviceRegGroupId).HasColumnName("device_reg_group_id");
      entity.Property(e => e.InspectionExpirationStatus)
              .HasDefaultValueSql("'prepare_to_inspection'::character varying")
              .HasColumnType("character varying")
              .HasColumnName("inspection_expiration_status");
      entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
      entity.Property(e => e.RoomId).HasColumnName("room_id");
      entity.Property(e => e.SerialId)
              .HasColumnType("character varying")
              .HasColumnName("serial_id");
      entity.Property(e => e.ServiceId).HasColumnName("service_id");
      entity.Property(e => e.Status)
              .HasDefaultValueSql("'in_stock'::character varying")
              .HasColumnType("character varying")
              .HasColumnName("status");
      entity.Property(e => e.SupplementaryKitId).HasColumnName("supplementary_kit_id");
      entity.Property(e => e.TabelId).HasColumnName("tabel_id");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");
      entity.Property(e => e.YearOfCommissioning).HasColumnName("year_of_commissioning");
      entity.Property(e => e.YearOfProduction).HasColumnName("year_of_production");

      entity.HasOne(d => d.DeviceModel).WithMany(p => p.Devices)
              .HasForeignKey(d => d.DeviceModelId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_ae5fa0b57f");

      entity.HasOne(d => d.DeviceRegGroup).WithMany(p => p.Devices)
              .HasForeignKey(d => d.DeviceRegGroupId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_1ac96c8753");

      entity.HasOne(d => d.Room).WithMany(p => p.Devices)
              .HasForeignKey(d => d.RoomId)
              .HasConstraintName("fk_rails_3824183ebe");

      entity.HasOne(d => d.Service).WithMany(p => p.Devices)
              .HasForeignKey(d => d.ServiceId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_ab5773a75f");

      entity.HasOne(d => d.SupplementaryKit).WithMany(p => p.Devices)
              .HasForeignKey(d => d.SupplementaryKitId)
              .HasConstraintName("fk_rails_60da5df21a");
    });

    modelBuilder.Entity<DeviceComponent>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("device_components_pkey");

      entity.ToTable("device_components");

      entity.HasIndex(e => e.SupplementaryKitId, "index_device_components_on_supplementary_kit_id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.Description).HasColumnName("description");
      entity.Property(e => e.MeasurementMax).HasColumnName("measurement_max");
      entity.Property(e => e.MeasurementMin).HasColumnName("measurement_min");
      entity.Property(e => e.MeasuringUnit)
              .HasColumnType("character varying")
              .HasColumnName("measuring_unit");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.SerialId)
              .HasColumnType("character varying")
              .HasColumnName("serial_id");
      entity.Property(e => e.SupplementaryKitId).HasColumnName("supplementary_kit_id");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");

      entity.HasOne(d => d.SupplementaryKit).WithMany(p => p.DeviceComponents)
              .HasForeignKey(d => d.SupplementaryKitId)
              .HasConstraintName("fk_rails_31b46de29b");
    });

    modelBuilder.Entity<DeviceModel>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("device_models_pkey");

      entity.ToTable("device_models");

      entity.HasIndex(e => e.ManufacturerId, "index_device_models_on_manufacturer_id");

      entity.HasIndex(e => e.MeasurementClassId, "index_device_models_on_measurement_class_id");

      entity.HasIndex(e => e.MeasurementGroupId, "index_device_models_on_measurement_group_id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.AccuracyClass).HasColumnName("accuracy_class");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.DocUrl)
              .HasColumnType("character varying")
              .HasColumnName("doc_url");
      entity.Property(e => e.ImageUrl)
              .HasColumnType("character varying")
              .HasColumnName("image_url");
      entity.Property(e => e.IsCompleteDevice).HasColumnName("is_complete_device");
      entity.Property(e => e.IsTapeRollingMechanism).HasColumnName("is_tape_rolling_mechanism");
      entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");
      entity.Property(e => e.MeasurementClassId).HasColumnName("measurement_class_id");
      entity.Property(e => e.MeasurementGroupId).HasColumnName("measurement_group_id");
      entity.Property(e => e.MeasurementMax).HasColumnName("measurement_max");
      entity.Property(e => e.MeasurementMin).HasColumnName("measurement_min");
      entity.Property(e => e.CalibrationMin).HasColumnName("calibration_min");
      entity.Property(e => e.CalibrationMax).HasColumnName("calibration_max");
      entity.Property(e => e.MeasurementSensitivity).HasColumnName("measurement_sensitivity");
      entity.Property(e => e.MeasuringUnit)
              .HasColumnType("character varying")
              .HasColumnName("measuring_unit");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.SafetyClass)
              .HasColumnType("character varying")
              .HasColumnName("safety_class");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");

      entity.HasOne(d => d.Manufacturer).WithMany(p => p.DeviceModels)
              .HasForeignKey(d => d.ManufacturerId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_806ab71b20");

      entity.HasOne(d => d.MeasurementClass).WithMany(p => p.DeviceModels)
              .HasForeignKey(d => d.MeasurementClassId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_ec1705823a");

      entity.HasOne(d => d.MeasurementGroup).WithMany(p => p.DeviceModels)
              .HasForeignKey(d => d.MeasurementGroupId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_cb28d0e4ea");
    });

    modelBuilder.Entity<DeviceRegGroup>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("device_reg_groups_pkey");

      entity.ToTable("device_reg_groups");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");
    });

    modelBuilder.Entity<Division>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("divisions_pkey");

      entity.ToTable("divisions");

      entity.HasIndex(e => e.OrganizationId, "index_divisions_on_organization_id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");

      entity.HasOne(d => d.Organization).WithMany(p => p.Divisions)
              .HasForeignKey(d => d.OrganizationId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_648c512956");
    });

    modelBuilder.Entity<History>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("histories_pkey");

      entity.ToTable("histories");

      entity.HasIndex(e => e.ChannelId, "index_histories_on_channel_id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.ChannelId).HasColumnName("channel_id");
      entity.Property(e => e.CreatedAt).HasColumnName("created_at");
      entity.Property(e => e.EventDatetime).HasColumnName("event_datetime");
      entity.Property(e => e.EventImpulseValue).HasColumnName("event_impulse_value");
      entity.Property(e => e.EventNotSystemValue).HasColumnName("event_not_system_value");
      entity.Property(e => e.EventSystemValue).HasColumnName("event_system_value");
      entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

      entity.HasOne(d => d.Channel).WithMany(p => p.Histories)
              .HasForeignKey(d => d.ChannelId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_7f43dbe0e4");
    });

    modelBuilder.Entity<Inspection>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("inspections_pkey");

      entity.ToTable("inspections");

      entity.HasIndex(e => e.CreatorId, "index_inspections_on_creator_id");

      entity.HasIndex(e => e.DeviceId, "index_inspections_on_device_id");

      entity.HasIndex(e => e.PerformerId, "index_inspections_on_performer_id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Conclusion)
              .HasColumnType("character varying")
              .HasColumnName("conclusion");
      entity.Property(e => e.ConclusionDate)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("conclusion_date");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.CreatorId).HasColumnName("creator_id");
      entity.Property(e => e.Description).HasColumnName("description");
      entity.Property(e => e.DeviceId).HasColumnName("device_id");
      entity.Property(e => e.PerformerId).HasColumnName("performer_id");
      entity.Property(e => e.State)
              .HasColumnType("character varying")
              .HasColumnName("state");
      entity.Property(e => e.TypeTarget)
              .HasColumnType("character varying")
              .HasColumnName("type_target");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");

      entity.HasOne(d => d.Creator).WithMany(p => p.InspectionCreators)
              .HasForeignKey(d => d.CreatorId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_5d7a8ba713");

      entity.HasOne(d => d.Device).WithMany(p => p.Inspections)
              .HasForeignKey(d => d.DeviceId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_183fe12b49");

      entity.HasOne(d => d.Performer).WithMany(p => p.InspectionPerformers)
              .HasForeignKey(d => d.PerformerId)
              .HasConstraintName("fk_rails_8573ab9766");
    });

    modelBuilder.Entity<Manufacturer>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("manufacturers_pkey");

      entity.ToTable("manufacturers");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Adress)
              .HasColumnType("character varying")
              .HasColumnName("adress");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.Email)
              .HasColumnType("character varying")
              .HasColumnName("email");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.Phone)
              .HasColumnType("character varying")
              .HasColumnName("phone");
      entity.Property(e => e.SiteUrl)
              .HasColumnType("character varying")
              .HasColumnName("site_url");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");
    });

    modelBuilder.Entity<MeasurementClass>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("measurement_classes_pkey");

      entity.ToTable("measurement_classes");

      entity.HasIndex(e => e.MeasurementGroupId, "index_measurement_classes_on_measurement_group_id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.ArmsDeviceType).HasColumnName("arms_device_type");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.MeasurementGroupId).HasColumnName("measurement_group_id");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");

      entity.HasOne(d => d.MeasurementGroup).WithMany(p => p.MeasurementClasses)
              .HasForeignKey(d => d.MeasurementGroupId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_45494ba51a");
    });

    modelBuilder.Entity<MeasurementGroup>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("measurement_groups_pkey");

      entity.ToTable("measurement_groups");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");
    });

    modelBuilder.Entity<Organization>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("organizations_pkey");

      entity.ToTable("organizations");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.Email)
              .HasColumnType("character varying")
              .HasColumnName("email");
      entity.Property(e => e.Fax)
              .HasColumnType("character varying")
              .HasColumnName("fax");
      entity.Property(e => e.FullAddress)
              .HasColumnType("character varying")
              .HasColumnName("full_address");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.Phone)
              .HasColumnType("character varying")
              .HasColumnName("phone");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");
      entity.Property(e => e.ZipCode)
              .HasColumnType("character varying")
              .HasColumnName("zip_code");
    });

    modelBuilder.Entity<Post>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("posts_pkey");

      entity.ToTable("posts");

      entity.HasIndex(e => e.UserId, "index_posts_on_user_id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Body).HasColumnName("body");
      entity.Property(e => e.Category)
              .HasColumnType("character varying")
              .HasColumnName("category");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.Title)
              .HasColumnType("character varying")
              .HasColumnName("title");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");
      entity.Property(e => e.UserId).HasColumnName("user_id");

      entity.HasOne(d => d.User).WithMany(p => p.Posts)
              .HasForeignKey(d => d.UserId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_5b5ddfd518");
    });

    modelBuilder.Entity<Room>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("rooms_pkey");

      entity.ToTable("rooms");

      entity.HasIndex(e => e.BuildingId, "index_rooms_on_building_id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.BuildingId).HasColumnName("building_id");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");

      entity.HasOne(d => d.Building).WithMany(p => p.Rooms)
              .HasForeignKey(d => d.BuildingId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_a3957b23a8");
    });

    modelBuilder.Entity<SchemaMigration>(entity =>
    {
      entity.HasKey(e => e.Version).HasName("schema_migrations_pkey");

      entity.ToTable("schema_migrations");

      entity.Property(e => e.Version)
              .HasColumnType("character varying")
              .HasColumnName("version");
    });

    modelBuilder.Entity<Server>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("servers_pkey");

      entity.ToTable("servers");

      entity.HasIndex(e => e.RoomId, "index_servers_on_room_id");

      entity.HasIndex(e => e.ServiceId, "index_servers_on_service_id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
      entity.Property(e => e.IpAdress)
              .HasColumnType("character varying")
              .HasColumnName("ip_adress");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.RoomId).HasColumnName("room_id");
      entity.Property(e => e.ServiceId).HasColumnName("service_id");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");

      entity.HasOne(d => d.Room).WithMany(p => p.Servers)
              .HasForeignKey(d => d.RoomId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_40364c1f8f");

      entity.HasOne(d => d.Service).WithMany(p => p.Servers)
              .HasForeignKey(d => d.ServiceId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_66cce08090");
    });

    modelBuilder.Entity<Service>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("services_pkey");

      entity.ToTable("services");

      entity.HasIndex(e => e.BuildingId, "index_services_on_building_id");

      entity.HasIndex(e => e.DivisionId, "index_services_on_division_id");

      entity.HasIndex(e => e.OrganizationId, "index_services_on_organization_id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.BuildingId).HasColumnName("building_id");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.DivisionId).HasColumnName("division_id");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");

      entity.HasOne(d => d.Building).WithMany(p => p.Services)
              .HasForeignKey(d => d.BuildingId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_76634691db");

      entity.HasOne(d => d.Division).WithMany(p => p.Services)
              .HasForeignKey(d => d.DivisionId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_f50c4560d9");

      entity.HasOne(d => d.Organization).WithMany(p => p.Services)
              .HasForeignKey(d => d.OrganizationId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_2e9f369e43");
    });

    modelBuilder.Entity<SupplementaryKit>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("supplementary_kits_pkey");

      entity.ToTable("supplementary_kits");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.Description)
              .HasColumnType("character varying")
              .HasColumnName("description");
      entity.Property(e => e.Name)
              .HasColumnType("character varying")
              .HasColumnName("name");
      entity.Property(e => e.SerialId)
              .HasColumnType("character varying")
              .HasColumnName("serial_id");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");
    });

    modelBuilder.Entity<User>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("users_pkey");

      entity.ToTable("users");

      entity.HasIndex(e => e.ResetPasswordToken, "index_users_on_reset_password_token").IsUnique();

      entity.HasIndex(e => e.ServiceId, "index_users_on_service_id");

      entity.HasIndex(e => e.TabelId, "index_users_on_tabel_id").IsUnique();

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.AvatarUrl)
              .HasColumnType("character varying")
              .HasColumnName("avatar_url");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.Email)
              .HasDefaultValueSql("''::character varying")
              .HasColumnType("character varying")
              .HasColumnName("email");
      entity.Property(e => e.EncryptedPassword)
              .HasDefaultValueSql("''::character varying")
              .HasColumnType("character varying")
              .HasColumnName("encrypted_password");
      entity.Property(e => e.FirstName)
              .HasColumnType("character varying")
              .HasColumnName("first_name");
      entity.Property(e => e.LastName)
              .HasColumnType("character varying")
              .HasColumnName("last_name");
      entity.Property(e => e.Phone)
              .HasColumnType("character varying")
              .HasColumnName("phone");
      entity.Property(e => e.RememberCreatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("remember_created_at");
      entity.Property(e => e.ResetPasswordSentAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("reset_password_sent_at");
      entity.Property(e => e.ResetPasswordToken)
              .HasColumnType("character varying")
              .HasColumnName("reset_password_token");
      entity.Property(e => e.Role)
              .HasDefaultValueSql("'default'::character varying")
              .HasColumnType("character varying")
              .HasColumnName("role");
      entity.Property(e => e.SecondName)
              .HasColumnType("character varying")
              .HasColumnName("second_name");
      entity.Property(e => e.ServiceId).HasColumnName("service_id");
      entity.Property(e => e.TabelId).HasColumnName("tabel_id");
      entity.Property(e => e.Timezone)
              .HasDefaultValueSql("'UTC'::character varying")
              .HasColumnType("character varying")
              .HasColumnName("timezone");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("timestamp(6) without time zone")
              .HasColumnName("updated_at");

      entity.HasOne(d => d.Service).WithMany(p => p.Users)
              .HasForeignKey(d => d.ServiceId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_rails_093eb6ba73");
    });

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
